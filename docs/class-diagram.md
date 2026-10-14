# Sơ đồ lớp sơ bộ — EduHub

Sơ đồ lớp 5 entity chính của hệ thống EduHub (Member, Club, Event, Registration, CheckIn) thể hiện thuộc tính, phương thức hành vi, các khoá ngoại (FK) và mối quan hệ.

---

## 1. Hình ảnh Sơ đồ lớp

![Sơ đồ lớp EduHub](SoDoLopEduHub.png)

---

## 2. Sơ đồ dạng Mermaid

```mermaid
classDiagram
    class Club {
        +Guid Id
        +string Name
        +string Description
        +DateTime CreatedAt
        +IReadOnlyList~Member~ Members
        +Create(string, string)$ Club
        +AddMember(Member)
        +RemoveMember(Guid) bool
        +ContainsMember(Guid) bool
        +MemberCount() int
    }

    class Member {
        +Guid Id
        +string Name
        +string Email
        +string StudentCode
        +DateTime JoinedAt
        +MemberStatus Status
        +Create(string, string, string)$ Member
        +IsActive() bool
        +Deactivate()
        +Activate()
    }

    class Event {
        +Guid Id
        +Guid ClubId
        +string Title
        +DateTime Date
        +int Capacity
        +bool IsOnline
        +Create(Guid, string, DateTime, int, bool)$ Event
        +HasCapacity(int) bool
        +RemainingCapacity(int) int
        +IsUpcoming() bool
    }

    class Registration {
        +Guid Id
        +Guid MemberId
        +Guid EventId
        +DateTime RegisteredAt
        +string Status
        +Create(Guid, Guid)$ Registration
        +Confirm()
        +Cancel()
        +IsConfirmed() bool
        +IsPending() bool
        +IsCancelled() bool
    }

    class CheckIn {
        +Guid Id
        +Guid RegistrationId
        +DateTime CheckedInAt
        +string Method
        +Create(Registration, string)$ CheckIn
        +Summary() string
    }

    Club "*" -- "*" Member : có thành viên
    Club "1" --> "*" Event : tổ chức (Event.ClubId)
    Member "1" --> "*" Registration : đăng ký (Registration.MemberId)
    Event "1" --> "*" Registration : nhận (Registration.EventId)
    Registration "1" --> "0..1" CheckIn : điểm danh (CheckIn.RegistrationId)
```
