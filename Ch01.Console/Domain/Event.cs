namespace EduHub.Ch01.Domain;

/// <summary>
/// Sự kiện do câu lạc bộ tổ chức.
/// </summary>
public class Event
{
    /// <summary>Định danh duy nhất của sự kiện.</summary>
    public Guid Id { get; private set; }

    /// <summary>Định danh câu lạc bộ tổ chức (khoá ngoại ClubId).</summary>
    public Guid ClubId { get; private set; }

    /// <summary>Tiêu đề của sự kiện.</summary>
    public string Title { get; private set; }

    /// <summary>Thời gian diễn ra sự kiện.</summary>
    public DateTime Date { get; private set; }

    /// <summary>Sức chứa số lượng người tham dự tối đa.</summary>
    public int Capacity { get; private set; }

    /// <summary>Sự kiện tổ chức trực tuyến (online) hay trực tiếp (offline).</summary>
    public bool IsOnline { get; private set; }

    /// <summary>Constructor riêng tư bảo vệ tính toàn vẹn của Event.</summary>
    private Event(Guid clubId, string title, DateTime date, int capacity, bool isOnline)
    {
        Id = Guid.NewGuid();
        ClubId = clubId;
        Title = title;
        Date = date;
        Capacity = capacity;
        IsOnline = isOnline;
    }

    /// <summary>
    /// Tạo mới một sự kiện với các ràng buộc dữ liệu hợp lệ.
    /// </summary>
    /// <param name="clubId">Mã CLB tổ chức (không được rỗng).</param>
    /// <param name="title">Tiêu đề sự kiện (không được rỗng).</param>
    /// <param name="date">Thời gian diễn ra sự kiện.</param>
    /// <param name="capacity">Sức chứa tối đa (phải lớn hơn 0).</param>
    /// <param name="isOnline">Hình thức tổ chức (mặc định false - offline).</param>
    /// <exception cref="ArgumentException">Bắn ra khi dữ liệu đầu vào không hợp lệ.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Bắn ra khi sức chứa không dương.</exception>
    public static Event Create(Guid clubId, string title, DateTime date, int capacity, bool isOnline = false)
    {
        if (clubId == Guid.Empty)
            throw new ArgumentException("Sự kiện phải thuộc về một CLB xác định (ClubId hợp lệ).", nameof(clubId));

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Tiêu đề sự kiện không được để trống.", nameof(title));

        if (capacity <= 0)
            throw new ArgumentOutOfRangeException(nameof(capacity), "Sức chứa sự kiện phải lớn hơn 0.");

        return new Event(clubId, title.Trim(), date, capacity, isOnline);
    }

    /// <summary>Kiểm tra sự kiện còn chỗ cho người đăng ký mới hay không.</summary>
    public bool HasCapacity(int registeredCount) => registeredCount < Capacity;

    /// <summary>Tính toán số lượng chỗ còn lại của sự kiện.</summary>
    public int RemainingCapacity(int registeredCount) => Math.Max(0, Capacity - registeredCount);

    /// <summary>Kiểm tra xem sự kiện có diễn ra trong tương lai hay không.</summary>
    public bool IsUpcoming() => Date > DateTime.UtcNow;

    /// <inheritdoc />
    public override string ToString() => $"{Title} ({Date:dd/MM/yyyy HH:mm}, Sức chứa: {Capacity}, {(IsOnline ? "Online" : "Offline")})";
}
