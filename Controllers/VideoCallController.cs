using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace VideoCallApp.Controllers
{
    public class VideoCallController : Controller
    {
        private readonly string appId = "YOUR_APP_ID";  // Lấy từ Agora Console
        private readonly string appCertificate = "YOUR_APP_CERTIFICATE";  // Lấy từ Agora Console

        // GET: VideoCall/Start
        public IActionResult Start(int roomId, int userId)
        {
            // Gửi dữ liệu video call, có thể gọi API của Agora ở đây nếu cần
            return View(new { RoomId = roomId, UserId = userId });
        }

        // GET: VideoCall/End
        public IActionResult End()
        {
            // Kết thúc cuộc gọi video, có thể làm việc với API Agora ở đây nếu cần
            return View();
        }
    }
}
