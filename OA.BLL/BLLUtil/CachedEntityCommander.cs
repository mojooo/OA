using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using CanYouLib.Exceptions;
using CanYouLib.Common;

namespace CanYou
{
    /*�������ֹ�����Subsonic�ײ㷽��������sql���������ݿ⣬����ᵼ���ڴ�����ݿ����ݵĲ�һ�´���*/
    public class CachedEntityCommander
    {

        public const string CacheKey = "CachedEntity_{0}";
        private static Hashtable ht = new Hashtable();

        public static void RegisterMemoryObject(Type type)
        {
            if (!(type.IsSubclassOf(typeof(EntityObject))))
            {
                throw new AppException("��ע��EntityObject����");
            }
            try
            {
                ht.Add(type, 1);
            }
            catch (ArgumentNullException )
            {
                throw new AppException("ע�������Ϊ��");
            }
            catch (ArgumentException )
            {
                //�ظ���ӣ�����
            }
        }
        internal static bool IsTypeRegistered(Type type)
        {
            return ht.ContainsKey(type);
        }
        internal static object GetCache(string cacheKey)
        {
            return DataCache.GetCache(cacheKey);
        }
        internal static void RemoveCache(string cacheKey)
        {
            DataCache.RemoveCache(cacheKey);
        }
        internal static void  SetCache(string cacheKey, object objObject)
        {
            DataCache.SetCache(cacheKey, objObject);  //��ʵ����Ŀ�п��Ը��Ĵ˷�����ʵ��
        }
        
    }
}
