namespace CarTroubleSolver.Logic.Dto.File
{
    public class FileDto
    {
        public FileDto(FileStream fileStream, string contentType, string fileName)
        {
            FileStream = fileStream;
            ContentType = contentType;
            FileName = fileName;
        }

        public FileStream? FileStream { get; set; }
        public string? ContentType { get; set; }
        public string? FileName { get; set; }
    }
}
