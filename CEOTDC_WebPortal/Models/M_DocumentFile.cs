namespace CEOTDC_WebPortal.Models
{
    public class M_DocumentFile
    {
        public int? id { get; set; }
        public int documentId { get; set; }
        public int documentTypeId { get; set; }
        public string filePath { get; set; }
        public string fileLinkExt { get; set; }
        public string fileName { get; set; }
        public M_Document documentObj { get; set; }
        public M_DocumentType documentTypeObj { get; set; }
    }
}
