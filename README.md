This is an Account Management System where we can manage Accounts, Vouchers and Users. This project has been created using ASP.NET Core Razor Pages using SQL Server and Stored Procedure without using any LINQ.
When someone runs the project for the first time, three roles will be created (Admin, Accountant, Viewer) and a user will be created username: admin@example.com pass: Admin@123
After running this project, we need to log in first. The user can also register.

Login Page:
![image](https://github.com/user-attachments/assets/0c5c10e3-76f6-498c-ae11-2b1f1dac559b)

Register Page:
![image](https://github.com/user-attachments/assets/724f783a-0dba-4b23-b007-4014d226dbaf)


After Logging in, we can see the Home page, and the user can navigate through pages based on their User Role. The admin can also manage the User Role.

Home Page for Admin:
![image](https://github.com/user-attachments/assets/ec9cbfc7-7994-4558-be35-fb86d3a210e9)

Home Page for Account:
![image](https://github.com/user-attachments/assets/cad88df5-ed90-4f4a-99f0-15a0509bfa73)

Home Page for Viewer:
![image](https://github.com/user-attachments/assets/afa39331-50ce-4134-9635-146b0da509cd)

If a user tries to navigate through the unauthorized page, they will see the Access Denied page:
![image](https://github.com/user-attachments/assets/dc13d6cd-66e7-41a1-89dc-2178041a1513)

This is the Chart of Accounts page where users can manage the Accounts by creating a New Account, editing an existing account or deleting an existing account:
![image](https://github.com/user-attachments/assets/a35b518b-79a7-4120-a515-e434c3856fc2)

We can also create a new Account without a ParentId or with a ParentId:
![image](https://github.com/user-attachments/assets/d9a3ee49-29b8-43c7-a7fc-dd905d4fedac)

This is the Account Tree view where the user can see the Tree View of the Accounts:
![image](https://github.com/user-attachments/assets/6f89d535-8b7d-4e79-ad51-fdba6f5b8494)

This is the Journal Voucher Creating Page:
![image](https://github.com/user-attachments/assets/23c0fe69-d84c-4279-8220-2261bbe94076)

Like Journal Voucher, we can create Payment Voucher and Receipt Voucher:
![image](https://github.com/user-attachments/assets/0b52f881-b6bd-40da-9a31-def56b49c9ef)

From All Vouchers, we will see the all created vouchers:
![image](https://github.com/user-attachments/assets/8774a0b3-1373-4cb7-9e44-9279a675b5c0)

From the View button, we can see the created Voucher details:
![image](https://github.com/user-attachments/assets/70971338-279d-44dd-ab16-a166f922856e)

Admin can also manage users and admin can create and manage roles.
This is the Manage Users page:
![image](https://github.com/user-attachments/assets/23b2d5d4-67e4-46ab-848f-7149d70fe207)

From the Edit user option, the user can change Name, Email and Role:
![image](https://github.com/user-attachments/assets/7f90f225-47d0-4a4d-977e-a6b82b327823)

This is the Manage Roles section:
![image](https://github.com/user-attachments/assets/c0787822-d999-4e12-af87-62e8f8551a1b)


















