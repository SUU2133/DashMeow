// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("kmmsEg0QXtaXqmcn3DqOv1PqnAcQTXaVxjfUooB1AfBZoBWtZefNU6zCzfhl13K/nEw0qQGEHvM7TyRD2mjryNrn7OPAbKJsHefr6+vv6ukkdIH8ghSsPPxmTcdhhSbX3BS/sX/n61/FWl4yEoMpOhlmp5LQwflLtXxXa9ZKyrxfBl7NONOsrxf+qywJDnMyd1m/3BKi8v8rpU+IlBkOT2jr5eraaOvg6Gjr6+pbfGzwzf+P4ZE+k6MWs2WkrQ0CAE1O7t37Do8uLz/RUAF450a+/XOFowW6ce1PgAEpoVtXCipGJj/kOrRuse1bQBsu601Dput+aJOGlw01IbcWU9mALSAsFKJodPr7Fpwl1yw3iuOn6DwqoHThz4Ha4FJJGejp6+rr");
        private static int[] order = new int[] { 5,4,7,5,8,5,11,11,8,11,13,11,12,13,14 };
        private static int key = 234;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
