using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using MugMatcher.ImageProvider;
using MugMatcher.Web.Models;

namespace MugMatcher.Web.Controllers
{
    public class FindController : Controller
    {
	    private const string MissingImageLocation = @"C:\SourceControl\HackManchester2016\TestImages\MissingPeople";

		public ActionResult Index()
        {
	        return View();
        }
		
        public async Task<JsonResult> FindPerson()
        {
            try
            {
                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        // get a stream
                        var stream = fileContent.InputStream;
                        // and optionally write the file to disk
                        var fileName = Path.GetFileName(file);
                        var path = Path.Combine("C:\\temp", fileName+ ".jpg");
                        using (var fileStream = System.IO.File.Create(path))
                        {
                            stream.CopyTo(fileStream);
                        }
	                    DeleteExistingTempFilesFiles();
						var acquisition = new Acquisition(new ImageStore());
						var mugMatcher = new MugMatcher(acquisition);
						var results = mugMatcher.Find(path, new ImageFetchRequest(null));
						return Json(results);
					}
				}
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Upload failed");
            }

            return Json(true);
		}

		private static void DeleteExistingTempFilesFiles()
		{
			var directoryInfo = new DirectoryInfo(ConfigurationManager.AppSettings["TemporaryImageStorePath"]);
			foreach (var file in directoryInfo.GetFiles())
				file.Delete();
		}

		public ActionResult Image(string file)
		{
			if (file == null)
				return null;
			var path = Directory.EnumerateFiles(ConfigurationManager.AppSettings["TemporaryImageStorePath"]).First(f => f.Contains(file));
			return File(path, "image/jpeg");
		}
	}
}