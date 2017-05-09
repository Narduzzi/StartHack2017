using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace OBUtils {
    public class Utils {

        public static void Swap<T>(ref T x, ref T y) {
            T t = y;
            y = x;
            x = t;
        }

        public static bool InRange(double x, double min, double max) {
            return x >= min && x <= max;
        }

        public static bool InRange(Decimal x, Decimal min, Decimal max) {
            return x >= min && x <= max;
        }

        public static bool InRangeExcl(double x, double min, double max) {
            return x > min && x < max;
        }

        public static bool InRangeExcl(Decimal x, Decimal min, Decimal max) {
            return x > min && x < max;
        }

    }

}