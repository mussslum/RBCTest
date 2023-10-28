namespace RBCTest.Models
{
    public class FileDto
    {
        public int FileId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile formFile { get; set; }
    }
}
