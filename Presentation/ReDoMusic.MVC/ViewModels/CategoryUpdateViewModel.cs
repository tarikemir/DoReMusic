namespace DoReMusic.MVC.ViewModels
{
    public class CategoryUpdateViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public List<String>? Kinds { get; set; }
    }
}
