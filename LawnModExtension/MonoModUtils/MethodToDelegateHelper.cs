using System;
using System.Collections.Generic;
using System.Text;

namespace LawnMod
{
    public static partial class MonoModUtils
    {
        public static class MethodToDelegateHelper
        {
            public delegate void MyDel(Lawn.SeedPacketsWidget arg1, Lawn.SeedType arg2, ref int arg3, ref int arg4);
            public delegate void MyDel_Outer(MyDel arg0, Lawn.SeedPacketsWidget arg1, Lawn.SeedType arg2, ref int arg3, ref int arg4);
            public static Delegate F<TResult>(dynamic theInputMethod)
            {
                return new Func<TResult>((Func<TResult>)theInputMethod);
            }

            public static Delegate F<T1, TResult>(dynamic theInputMethod)
            {
                return new Func<T1, TResult>((Func<T1, TResult>)theInputMethod);
            }

            public static Delegate F<T1, T2, TResult>(dynamic theInputMethod)
            {
                return new Func<T1, T2, TResult>((Func<T1, T2, TResult>)theInputMethod);
            }

            public static Delegate F<T1, T2, T3, TResult>(dynamic theInputMethod)
            {
                return new Func<T1, T2, T3, TResult>((Func<T1, T2, T3, TResult>)theInputMethod);
            }

            public static Delegate F<T1, T2, T3, T4, TResult>(dynamic theInputMethod)
            {
                return new Func<T1, T2, T3, T4, TResult>((Func<T1, T2, T3, T4, TResult>)theInputMethod);
            }

            public static Delegate F<T1, T2, T3, T4, T5, TResult>(dynamic theInputMethod)
            {
                return new Func<T1, T2, T3, T4, T5, TResult>((Func<T1, T2, T3, T4, T5, TResult>)theInputMethod);
            }

            public static Delegate F<T1, T2, T3, T4, T5, T6, TResult>(dynamic theInputMethod)
            {
                return new Func<T1, T2, T3, T4, T5, T6, TResult>((Func<T1, T2, T3, T4, T5, T6, TResult>)theInputMethod);
            }

            public static Delegate F<T1, T2, T3, T4, T5, T6, T7, TResult>(dynamic theInputMethod)
            {
                return new Func<T1, T2, T3, T4, T5, T6, T7, TResult>((Func<T1, T2, T3, T4, T5, T6, T7, TResult>)theInputMethod);
            }

            public static Delegate F<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(dynamic theInputMethod)
            {
                return new Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>((Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>)theInputMethod);
            }

            public static Delegate F<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(dynamic theInputMethod)
            {
                return new Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>((Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>)theInputMethod);
            }

            public static Delegate F<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(dynamic theInputMethod)
            {
                return new Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>((Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>)theInputMethod);
            }

            public static Delegate F<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(dynamic theInputMethod)
            {
                return new Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>((Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>)theInputMethod);
            }

            public static Delegate F<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(dynamic theInputMethod)
            {
                return new Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>((Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>)theInputMethod);
            }

            public static Delegate F<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(dynamic theInputMethod)
            {
                return new Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>((Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>)theInputMethod);
            }

            public static Delegate F<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(dynamic theInputMethod)
            {
                return new Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>((Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>)theInputMethod);
            }

            public static Delegate F<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(dynamic theInputMethod)
            {
                return new Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>((Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>)theInputMethod);
            }

            public static Delegate F<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(dynamic theInputMethod)
            {
                return new Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>((Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>)theInputMethod);
            }

            public static Delegate A<T1>(dynamic theInputMethod)
            {
                return new Action<T1>((Action<T1>)theInputMethod);
            }

            public static Delegate A<T1, T2>(dynamic theInputMethod)
            {
                return new Action<T1, T2>((Action<T1, T2>)theInputMethod);
            }

            public static Delegate A<T1, T2, T3>(dynamic theInputMethod)
            {
                return new Action<T1, T2, T3>((Action<T1, T2, T3>)theInputMethod);
            }

            public static Delegate A<T1, T2, T3, T4>(dynamic theInputMethod)
            {
                return new Action<T1, T2, T3, T4>((Action<T1, T2, T3, T4>)theInputMethod);
            }

            public static Delegate A<T1, T2, T3, T4, T5>(dynamic theInputMethod)
            {
                return new Action<T1, T2, T3, T4, T5>((Action<T1, T2, T3, T4, T5>)theInputMethod);
            }

            public static Delegate A<T1, T2, T3, T4, T5, T6>(dynamic theInputMethod)
            {
                return new Action<T1, T2, T3, T4, T5, T6>((Action<T1, T2, T3, T4, T5, T6>)theInputMethod);
            }

            public static Delegate A<T1, T2, T3, T4, T5, T6, T7>(dynamic theInputMethod)
            {
                return new Action<T1, T2, T3, T4, T5, T6, T7>((Action<T1, T2, T3, T4, T5, T6, T7>)theInputMethod);
            }

            public static Delegate A<T1, T2, T3, T4, T5, T6, T7, T8>(dynamic theInputMethod)
            {
                return new Action<T1, T2, T3, T4, T5, T6, T7, T8>((Action<T1, T2, T3, T4, T5, T6, T7, T8>)theInputMethod);
            }

            public static Delegate A<T1, T2, T3, T4, T5, T6, T7, T8, T9>(dynamic theInputMethod)
            {
                return new Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>((Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>)theInputMethod);
            }

            public static Delegate A<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(dynamic theInputMethod)
            {
                return new Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>((Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>)theInputMethod);
            }

            public static Delegate A<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(dynamic theInputMethod)
            {
                return new Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>((Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>)theInputMethod);
            }

            public static Delegate A<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(dynamic theInputMethod)
            {
                return new Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>((Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>)theInputMethod);
            }

            public static Delegate A<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(dynamic theInputMethod)
            {
                return new Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>((Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>)theInputMethod);
            }

            public static Delegate A<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(dynamic theInputMethod)
            {
                return new Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>((Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>)theInputMethod);
            }

            public static Delegate A<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(dynamic theInputMethod)
            {
                return new Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>((Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>)theInputMethod);
            }

            public static Delegate A<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(dynamic theInputMethod)
            {
                return new Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>((Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>)theInputMethod);
            }
        }
    }
}
