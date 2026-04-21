Mô Tả:
Một dự án game Top-down Shooter được xây dựng với tư duy Data Driven và Scalable Architecture.
Tập trung vào việc tách biệt logic để dễ dàng mở rộng tính năng mà không làm hỏng cấu trúc có sẵn.

- Pattern:

+SpawnSystem Sử dụng kết hợp Strategy Pattern và Factory Pattern.
+ các Script giao tiếp với nhau bằng Event và SingleTon để giảm phụ thuộc và doubling code

- Các hệ thống chính:

+ Item: Xây dựng theo hướng (Data-Driven) , Các vũ khí được tách biệt hành vi và dữ liệu và liên kết bằng mapping turn on turn off từ động phù hợp với dữ liệu khi nhận 

+ Skill System:```mermaid
classDiagram
    class SkillBase {
        <<Abstract>>
        +Data_SkillBase data
        +Execute()
    }
    class ActiveSkill {
        +Trigger()
    }
    class PassiveSkill {
        +UpdateAttribute()
    }
    SkillBase <|-- ActiveSkill
    SkillBase <|-- PassiveSkill

+ Save System : tách biệt RunTime Data , có cơ chế lưu game tự động khi qua màn ( chuyển thành chuỗi json , rồi lưu vào máy user )

+ Inventory System : kho đồ có hệ thống Đệ Quy tự động hoàn trả số lượng dư khi full
  Khi nhặt đồ Item -> Inventory Manager -> Item Panel[]

  

