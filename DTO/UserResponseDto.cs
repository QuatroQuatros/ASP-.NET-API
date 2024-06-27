namespace GestaoDeResiduos.DTO
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Role { get; set; }
    }
}
