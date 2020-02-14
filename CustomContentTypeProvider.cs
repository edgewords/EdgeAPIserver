// This custom class is used so that the .json extension is recognised as a file type
//when we download .json files with the File Server options
using Microsoft.Owin.StaticFiles.ContentTypes;

namespace EdgeAPIserver
{
    class CustomContentTypeProvider : FileExtensionContentTypeProvider
    {
        public CustomContentTypeProvider()
        {
            Mappings.Add(".json", "application/json");
        }
    }
}
