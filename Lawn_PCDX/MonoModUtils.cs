using System;

namespace Lawn
{
    public static class MonoModUtils
    {
        /* 
         * Purpose: process dynamic functions from scripting engine (ironpython etc.) and
         * turns it into a valid delegate for MonoMod.RuntimeDetour.
         */
        public static Func<dynamic, Delegate> AsDelegate(Type delegateType)
        {
            return (hookerDynScriptFunc) =>
            {
                Type[] generics = delegateType.GetGenericArguments();
                Type t_his = typeof(MonoModUtils);
                return (Delegate)t_his
                    .GetMethod("D", generics.Length, new Type[] { typeof(object) })
                    .MakeGenericMethod(generics)
                    .Invoke(t_his, new object[] { hookerDynScriptFunc });
            };
        }

        public static Delegate D<TResult>(dynamic theInputMethod)
        {
            return new Func<TResult>((Func<TResult>)theInputMethod);
        }

        public static Delegate D<T1, TResult>(dynamic theInputMethod)
        {
            return new Func<T1, TResult>((Func<T1, TResult>)theInputMethod);
        }

        public static Delegate D<T1, T2, TResult>(dynamic theInputMethod)
        {
            return new Func<T1, T2, TResult>((Func<T1, T2, TResult>)theInputMethod);
        }

        public static Delegate D<T1, T2, T3, TResult>(dynamic theInputMethod)
        {
            return new Func<T1, T2, T3, TResult>((Func<T1, T2, T3, TResult>)theInputMethod);
        }

        public static Delegate D<T1, T2, T3, T4, TResult>(dynamic theInputMethod)
        {
            return new Func<T1, T2, T3, T4, TResult>((Func<T1, T2, T3, T4, TResult>)theInputMethod);
        }

        public static Delegate D<T1, T2, T3, T4, T5, TResult>(dynamic theInputMethod)
        {
            return new Func<T1, T2, T3, T4, T5, TResult>((Func<T1, T2, T3, T4, T5, TResult>)theInputMethod);
        }

        public static Delegate D<T1, T2, T3, T4, T5, T6, TResult>(dynamic theInputMethod)
        {
            return new Func<T1, T2, T3, T4, T5, T6, TResult>((Func<T1, T2, T3, T4, T5, T6, TResult>)theInputMethod);
        }

        public static Delegate D<T1, T2, T3, T4, T5, T6, T7, TResult>(dynamic theInputMethod)
        {
            return new Func<T1, T2, T3, T4, T5, T6, T7, TResult>((Func<T1, T2, T3, T4, T5, T6, T7, TResult>)theInputMethod);
        }

        public static Delegate D<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(dynamic theInputMethod)
        {
            return new Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>((Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>)theInputMethod);
        }

        public static Delegate D<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(dynamic theInputMethod)
        {
            return new Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>((Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>)theInputMethod);
        }

        public static Delegate D<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(dynamic theInputMethod)
        {
            return new Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>((Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>)theInputMethod);
        }

        public static Delegate D<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(dynamic theInputMethod)
        {
            return new Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>((Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>)theInputMethod);
        }

        public static Delegate D<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(dynamic theInputMethod)
        {
            return new Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>((Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>)theInputMethod);
        }

        public static Delegate D<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(dynamic theInputMethod)
        {
            return new Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>((Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>)theInputMethod);
        }

        public static Delegate D<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(dynamic theInputMethod)
        {
            return new Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>((Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>)theInputMethod);
        }

        public static Delegate D<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(dynamic theInputMethod)
        {
            return new Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>((Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>)theInputMethod);
        }

        public static Delegate D<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(dynamic theInputMethod)
        {
            return new Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>((Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>)theInputMethod);
        }
    }
}
