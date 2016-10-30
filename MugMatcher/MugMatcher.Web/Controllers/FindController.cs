using System.IO;
using System.Linq;
using System.Web.Mvc;
using MugMatcher.ImageProvider;
using MugMatcher.Web.Models;

namespace MugMatcher.Web.Controllers
{
    public class FindController : Controller
    {
	    private const string MissingImageLocation = @"C:\Users\robert.marshall.FIOFFICE\Documents\HackManchester\TestImages\MissingPeople";

		public ActionResult Index()
        {
	        var fullPath = Path.GetFullPath(MissingImageLocation);
	        var model = new FindViewModel {FileList = Directory.EnumerateFiles(fullPath).Select(Path.GetFileNameWithoutExtension)};
	        return View(model);
        }

	    public JsonResult FindPerson(string file)
	    {
		    var imageFetcher=new LocalImageFetcher();
		    var mugMatcher=new MugMatcher(imageFetcher);
			var path = Directory.EnumerateFiles(MissingImageLocation).First(f => f.Contains(file));
			var found = mugMatcher.Find(path, new ImageFetchRequest(null));
			return Json(found);
	    }

		public ActionResult Image(string file)
		{
			if (file == null)
				return null;
			var path = Directory.EnumerateFiles(MissingImageLocation).First(f => f.Contains(file));
			return File(path, "image/jpeg");
		}
	}
}