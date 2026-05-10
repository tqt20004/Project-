Mô Tả:
Một dự án game Top-down Shooter được xây dựng với tư duy Data Driven và Scalable Architecture.
Tập trung vào việc tách biệt logic để dễ dàng mở rộng tính năng mà không làm hỏng cấu trúc có sẵn.

- Pattern:

+SpawnSystem Sử dụng kết hợp Strategy Pattern và Factory Pattern.
+các Script giao tiếp với nhau bằng Event và SingleTon để giảm phụ thuộc và doubling code

- Các hệ thống chính:

+ Item: Xây dựng theo hướng (Data-Driven) , Các vũ khí được tách biệt hành vi và dữ liệu và liên kết bằng mapping turn on turn off từ động phù hợp với dữ liệu khi nhận
- UML:https://github.com/tqt20004/my-portflolio/blob/main/WeaponSystem.png
  
+ Skill System:

AiBase & SkillBase (The Actors): * AiBase đóng vai trò là thực thể gốc (Container), quản lý danh sách các SkillBase.
SkillBase giữ logic thực thi và tham chiếu ngược lại owner (AiBase) để truy xuất các thông tin cần thiết khi tung chiêu.
RoleCfg & SkillCfg (The Data): * Đây là "linh hồn" của hệ thống dưới dạng Data-Driven. RoleCfg định nghĩa bộ kỹ năng khởi tạo cho một Role.
SkillCfg chứa toàn bộ thông số kỹ thuật (range, cooldown, effects, params per level). Khi AiBase khởi tạo, nó sẽ "đổ" dữ liệu từ Cfg vào Base tương ứng để vận hành.
Sự kết hợp (The Mapping): * Hệ thống sử dụng CodeName và Level làm chìa khóa (Key). ConfigManager sẽ map dữ liệu từ SkillInfo/RoleConfig để tìm đúng SkillCfg/RoleCfg, giúp việc thay đổi chỉ số hay thêm tướng/kỹ năng mới chỉ cần thông qua file cấu hình mà không cần chạm vào Source Code.
RoleStats (The Stat Manager): * Đóng vai trò là bộ máy tính toán chỉ số tập trung. Nó quản lý một Dictionary các StatConfigBase.
Điểm hay: Chỉ số được tính toán dựa trên công thức lũy tiến (Base * Percent + Value). Việc tách riêng RoleStats giúp AiBase không cần quan tâm đến logic tính toán phức tạp, chỉ cần truy vấn kết quả cuối cùng để áp dụng vào gameplay.
- UML: https://github.com/tqt20004/my-portflolio/blob/main/UML_SkillSystem.png



+ Save System : tách biệt RunTime Data , có cơ chế lưu game tự động khi qua màn ( chuyển thành chuỗi json , rồi lưu vào máy user )

+ Inventory System : kho đồ có hệ thống Đệ Quy tự động hoàn trả số lượng dư khi full
  Khi nhặt đồ Item -> Inventory Manager -> Item Panel[]

  

- Nhược điểm :
+ Đang trong quá trình phát triển , hoàn thiện .Nhiều đoạn rối , chưa refactor 
