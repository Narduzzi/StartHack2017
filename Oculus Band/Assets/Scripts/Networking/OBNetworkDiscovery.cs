using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Networking;

namespace OBNet {

    public class NetworkGame {

        public string addr;
        public string port;

        public string name;

        public NetworkGame() { }

        public NetworkGame(string addr, string port, string name) {
            this.addr = addr;
            this.port = port;
            this.name = name;
        }
    }

    /// <summary>
    /// Subclass of NetworkDiscovery that has methods added to 
    /// collect the games found during execution.
    /// </summary>
    public class OBNetworkDiscovery : NetworkDiscovery {
        /// <summary>
        /// Defines the amount of time for which we should look for a game
        /// on the LAN.
        /// </summary>
        public float discoveryTime = 1.0f;

        /// <summary>The list of games found.</summary>
        private List<NetworkGame> gamesFound = new List<NetworkGame>();

        private Action<List<NetworkGame>> m_callback;
        private string gameName;

        /// <summary>
        /// Returns the number of games were discovered during execution.
        /// </summary>
        /// <returns>Number of games found</returns>
        public int Count {
            get { return gamesFound.Count; }
        }

        /// <summary>
        /// Returns the list of game addresses it has found during execution.
        /// </summary>
        /// <returns>A List&lt;string&gt; containing the addresses.</returns>
        public List<NetworkGame> GetGames() {
            return new List<NetworkGame>(gamesFound);
        }

        /// <summary>
        /// Resets the list of games found.
        /// </summary>
        public void Clear() {
            gamesFound.Clear();
        }


        void Start() {
            if (base.showGUI) {
                Debug.LogWarning("UseGUI is enabled in OBNetworkDiscovery.");
            }
        }


        /// <summary>
        /// Overriding Initialize method for additional initialization
        /// </summary>
        public new void Initialize() {
            base.Initialize();

            NetworkManager netMngr = NetworkManager.singleton;
            if (netMngr != null) {
                string ip = netMngr.networkAddress;
                int port = netMngr.networkPort;

                if (gameName == null) {
                    base.broadcastData = ip + ":" + port + ":" + Environment.MachineName;
                } else {
                    base.broadcastData = ip + ":" + port + ":" + gameName;
                }
            }
        }


        /// <summary>
        /// Change the name of the local server appearing to others.
        /// </summary>
        /// <param name="name">New name</param>
        public void SetGameName(string name) {
            if (base.useNetworkManager) {
                Debug.LogError("Can't change match name when useNetworkManager is set to true.");
                return;
            }

            NetworkManager netMngr = NetworkManager.singleton;
            if (netMngr == null) {
                Debug.LogError("No network manager could be found in the scene.");
                return;
            }

            if (isServer) {
                Debug.LogError("Can't change broadcast data while running");
                return;
            }

            if (name.Contains(":")) {
                Debug.LogError("Game name can't contain ':'.");
                return;
            }

            gameName = name;
        }

        /// <summary>
        /// Event triggered when a new discovery server is detected.
        /// </summary>
        /// <param name="fromAddress">Local address of the server</param>
        /// <param name="data">Data package received from server</param>
        public override void OnReceivedBroadcast(string fromAddress, string data) {
            string[] parts = data.Split(':');

            string addr = parts[0];
            string port = parts[1];
            string name = parts[2];

            gamesFound.Add(new NetworkGame(addr, port, name));
        }



        /// <summary>
        /// Discover new games with callback
        /// </summary>
        /// <param name="callback">Method that takes a List&lt;NetworkGame&gt; as parameter.</param>
        public void DiscoverGames(Action<List<NetworkGame>> callback) {
            if (m_callback == null && callback != null) {
                m_callback = callback;

                StartCoroutine(DiscoverGamesRoutine());
            } else {
                Debug.LogWarning("There is already a network discovery in progress.");
            }
        }


        private void CleanGamesFound() {
            List<NetworkGame> games = new List<NetworkGame>();
            
            foreach(NetworkGame netGame in gamesFound) {
                bool found = false;

                foreach(NetworkGame netGameClean in games) {
                    if (netGame.addr == netGameClean.addr &&
                        netGame.port == netGameClean.port &&
                        netGame.name == netGameClean.name) {

                        found = true;
                        break;
                    }
                }

                if (!found) {
                    games.Add(netGame);
                }
            }

            gamesFound = games;
        }


        /// <summary>
        /// Coroutine function that starts the network discovery, waits a few seconds and
        /// collects all the detected games on the LAN.
        /// </summary>
        /// <returns>The coroutine</returns>
        public IEnumerator DiscoverGamesRoutine() {
            Initialize();
            Clear();
            StartAsClient();
            yield return new WaitForSeconds(discoveryTime);

            StopBroadcast();

            CleanGamesFound();

            if (m_callback != null) {
                m_callback.Invoke(gamesFound);
            }

            yield return null;
        }

    }

}