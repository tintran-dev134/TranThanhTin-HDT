using EduHub.Ch01.Domain;

// ============================================================================
// EduHub Ch01 — Console Demo (Nhiệm vụ 4: Luồng nghiệp vụ & Báo cáo tham dự)
// Tuân thủ triệt để tính đóng gói (encapsulation) và các bất biến miền (domain invariants)
// ============================================================================

const string Separator = "================================================================================";
const string SubSeparator = "--------------------------------------------------------------------------------";

static void PrintStep(int number, string title)
{
    Console.WriteLine($"\n{Separator}");
    Console.WriteLine($" [BUOC {number}] {title.ToUpper()}");
    Console.WriteLine(Separator);
}

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.WriteLine(Separator);
Console.WriteLine("        HE THONG QUAN LY CAU LAC BO & SU KIEN SINH VIEN - EDUHUB        ");
Console.WriteLine(Separator);

// ----------------------------------------------------------------------------
// BUOC 1: Tao Cau lac bo
// ----------------------------------------------------------------------------
PrintStep(1, "Khoi tao Cau lac bo");
var club = Club.Create(
    name: "Tech Club",
    description: "Cau lac bo Cong nghe sinh vien: Nghien cuu AI, Cloud Computing, Lap trinh va Hackathon.");

Console.WriteLine("[OK] Khoi tao thanh cong CLB:");
Console.WriteLine($"   - Ma CLB (Id):   {club.Id}");
Console.WriteLine($"   - Ten CLB:       {club.Name}");
Console.WriteLine($"   - Gioi thieu:    {club.Description}");
Console.WriteLine($"   - Ngay thanh lap:{club.CreatedAt:dd/MM/yyyy HH:mm:ss} UTC");

// ----------------------------------------------------------------------------
// BUOC 2: Them 3 thanh vien vao CLB
// ----------------------------------------------------------------------------
PrintStep(2, "Them 3 thanh vien vao Cau lac bo");
var members = new[]
{
    Member.Create("Nguyễn Văn An", "an.nv@student.dthu.edu.vn", "DH24001"),
    Member.Create("Trần Thị Bình", "binh.tt@student.dthu.edu.vn", "DH24002"),
    Member.Create("Lê Hoàng Cường", "cuong.lh@student.dthu.edu.vn", "DH24003")
};

foreach (var member in members)
{
    club.AddMember(member);
    Console.WriteLine($"   + Da ket nap: {member.Name,-16} | MSSV: {member.StudentCode,-8} | Email: {member.Email}");
}
Console.WriteLine($"\n[INFO] Tong so thanh vien trong '{club.Name}': {club.MemberCount()} sinh vien.");

// ----------------------------------------------------------------------------
// BUOC 3: Tao su kien do CLB to chuc
// ----------------------------------------------------------------------------
PrintStep(3, "Tao su kien do CLB to chuc");
const int eventCapacity = 50;
var workshop = Event.Create(
    clubId: club.Id,
    title: "Workshop Chinh phuc AI va Ung dung Thuc te",
    date: DateTime.UtcNow.AddDays(7).Date.AddHours(14), // 14:00 sau 7 ngay
    capacity: eventCapacity,
    isOnline: false);

Console.WriteLine("[OK] Su kien duoc tao thanh cong:");
Console.WriteLine($"   - Ma su kien:    {workshop.Id}");
Console.WriteLine($"   - Ten su kien:   {workshop.Title}");
Console.WriteLine($"   - Thoi gian:     {workshop.Date:dd/MM/yyyy HH:mm} UTC");
Console.WriteLine($"   - Suc chua:      {workshop.Capacity} cho ngoi");
Console.WriteLine($"   - Hinh thuc:     {(workshop.IsOnline ? "Truc tuyen (Online)" : "Truc tiep (Offline)")}");

// ----------------------------------------------------------------------------
// BUOC 4: 2 thanh vien dang ky tham du (Trang thai ban dau: Pending)
// ----------------------------------------------------------------------------
PrintStep(4, "Dang ky tham du su kien");
var registrations = new List<Registration>();
var memberLookup = members.ToDictionary(m => m.Id);

foreach (var member in members.Take(2))
{
    if (!workshop.HasCapacity(registrations.Count))
    {
        Console.WriteLine($"[CANH BAO] Su kien da kin cho! Khong the tiep nhan dang ky cho {member.Name}.");
        continue;
    }

    var reg = Registration.Create(member.Id, workshop.Id);
    registrations.Add(reg);
    Console.WriteLine($"   + Sinh vien [{member.StudentCode}] {member.Name,-16} dang ky thanh cong -> Trang thai: [{reg.Status}] (Cho duyet)");
}
Console.WriteLine($"\n[INFO] So phieu dang ky da ghi nhan: {registrations.Count}/{workshop.Capacity} (Con trong: {workshop.RemainingCapacity(registrations.Count)} cho).");

// ----------------------------------------------------------------------------
// BUOC 5: Ban to chuc xac nhan cac phieu dang ky
// ----------------------------------------------------------------------------
PrintStep(5, "Ban to chuc duyet va xac nhan dang ky");
foreach (var reg in registrations)
{
    reg.Confirm();
    var student = memberLookup[reg.MemberId];
    Console.WriteLine($"   [OK] Duyet phieu [{reg.Id.ToString()[..8]}] cho sinh vien {student.Name,-16} -> Trang thai moi: [{reg.Status}]");
}

// ----------------------------------------------------------------------------
// BUOC 6: Thanh vien check-in tham du qua ma QR
// ----------------------------------------------------------------------------
PrintStep(6, "Sinh vien thuc hien diem danh (Check-in)");
var checkIns = new List<CheckIn>();

// Sinh vien dau tien (Nguyen Van An) thuc hien quet ma QR check-in tai hoi truong
var checkInAn = CheckIn.Create(registrations[0], CheckInMethod.QrCode);
checkIns.Add(checkInAn);
Console.WriteLine($"   - {memberLookup[registrations[0].MemberId].Name}: {checkInAn.Summary()}");

// ----------------------------------------------------------------------------
// BUOC 7: Bao cao tong ket tham du su kien
// ----------------------------------------------------------------------------
PrintStep(7, "Bao cao tong ket tham du su kien");
Console.WriteLine($"Ten su kien: {workshop.Title}");
Console.WriteLine($"Thoi gian:   {workshop.Date:dd/MM/yyyy HH:mm} UTC | CLB chu quan: {club.Name}");
Console.WriteLine(SubSeparator);
Console.WriteLine("THONG KE CHUNG:");
Console.WriteLine($"   - Suc chua toi da:           {workshop.Capacity} cho");
Console.WriteLine($"   - Tong so phieu dang ky:     {registrations.Count} (Ty le lap day: {(double)registrations.Count / workshop.Capacity * 100:F1}%)");
Console.WriteLine($"   - Tong so da diem danh:      {checkIns.Count} (Ty le tham du thuc te: {(double)checkIns.Count / registrations.Count * 100:F1}%)");
Console.WriteLine($"   - So vang mat:               {registrations.Count - checkIns.Count}");
Console.WriteLine(SubSeparator);
Console.WriteLine("CHI TIET TUNG PHIEU DANG KY:");
Console.WriteLine($"{"Ma Phieu",-10} | {"MSSV",-8} | {"Ho va Ten",-16} | {"Dang ky",-11} | {"Diem danh",-15} | {"Chi tiet"}");
Console.WriteLine(new string('-', 80));

foreach (var reg in registrations)
{
    var student = memberLookup[reg.MemberId];
    var attendance = checkIns.FirstOrDefault(c => c.RegistrationId == reg.Id);
    var checkInStatus = attendance is not null ? "DA CHECK-IN" : "CHUA CHECK-IN";
    var detail = attendance is not null ? $"{attendance.Method} luc {attendance.CheckedInAt:HH:mm:ss}" : "Chua co mat";

    Console.WriteLine($"{reg.Id.ToString()[..8],-10} | {student.StudentCode,-8} | {student.Name,-16} | {reg.Status,-11} | {checkInStatus,-15} | {detail}");
}

// ----------------------------------------------------------------------------
// BUOC 8: Kiem thu toan dien cac quy tac nghiep vu (Invariants Verification)
// ----------------------------------------------------------------------------
PrintStep(8, "Kiem thu cac quy tac nghiep vu va bat bien (Business Invariants)");

// Thu nghiem 8.1: Bat bien vong doi phieu - Phieu da huy thi khong duoc xac nhan lai
Console.WriteLine("Thu nghiem 8.1: Huy phieu thu 2, sau do thu goi Confirm() vi pham:");
registrations[1].Cancel();
Console.WriteLine($"   Phieu cua {memberLookup[registrations[1].MemberId].Name} da chuyen trang thai thanh: [{registrations[1].Status}]");
try
{
    registrations[1].Confirm();
    Console.WriteLine("   [THAT BAI] Loi: Le ra khong duoc phep xac nhan phieu da huy!");
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"   [THANH CONG] Bat loi thanh cong nhu mong doi: \"{ex.Message}\"");
}

// Thu nghiem 8.2: Bat bien diem danh - Chi phieu Confirmed moi duoc phep diem danh
Console.WriteLine("\nThu nghiem 8.2: Co tinh check-in phieu vua bi Cancelled:");
try
{
    var invalidCheckIn = CheckIn.Create(registrations[1], CheckInMethod.Manual);
    Console.WriteLine("   [THAT BAI] Loi: Le ra khong duoc phep check-in phieu da huy!");
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"   [THANH CONG] Bat loi thanh cong nhu mong doi: \"{ex.Message}\"");
}

// Thu nghiem 8.3: Bat bien trung lap - Chan them thanh vien co trung MSSV vao CLB
Console.WriteLine("\nThu nghiem 8.3: Co tinh them thanh vien co trung ma sinh vien vao CLB:");
try
{
    var duplicateStudent = Member.Create("Nguyễn An Clone", "an.clone@student.dthu.edu.vn", "DH24001");
    club.AddMember(duplicateStudent);
    Console.WriteLine("   [THAT BAI] Loi: Le ra phai chan trung lap sinh vien!");
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"   [THANH CONG] Bat loi thanh cong nhu mong doi: \"{ex.Message}\"");
}

// Thu nghiem 8.4: Kiem tra tinh hop le du lieu dau vao (Validation)
Console.WriteLine("\nThu nghiem 8.4: Co tinh tao Member voi email sai dinh dang:");
try
{
    var invalidMember = Member.Create("Lỗi Email", "email-khong-co-a-cong", "DH24999");
    Console.WriteLine("   [THAT BAI] Loi: Le ra phai chan email khong hop le!");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"   [THANH CONG] Bat loi validation thanh cong: \"{ex.Message}\"");
}

Console.WriteLine($"\n{Separator}");
Console.WriteLine("TOAN BO 8 BUOC DEMO NGHIEP VU & KIEM THU BAT BIEN DA HOAN THANH.");
Console.WriteLine(Separator);


