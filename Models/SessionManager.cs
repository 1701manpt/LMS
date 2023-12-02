using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace LMS.Models
{
    public class SessionManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // Lưu trữ một danh sách JSON trong Session
        public void SetSessionData<T>(string key, List<T> dataList)
        {
            string jsonData = JsonConvert.SerializeObject(dataList);
            _httpContextAccessor.HttpContext.Session.SetString(key, jsonData);
        }

        // Lấy danh sách JSON từ Session
        public List<T> GetSessionData<T>(string key)
        {
            string jsonData = _httpContextAccessor.HttpContext.Session.GetString(key);
            if (jsonData != null)
            {
                return JsonConvert.DeserializeObject<List<T>>(jsonData);
            }
            return null;
        }

        // Xóa dữ liệu JSON từ Session
        public void RemoveSessionData(string key)
        {
            _httpContextAccessor.HttpContext.Session.Remove(key);
        }

        public bool IsExist(string key)
        {
            if (_httpContextAccessor.HttpContext.Session.TryGetValue(key, out var existingData))
            {
                return true;
            }
            return false;
        }
    }

}
