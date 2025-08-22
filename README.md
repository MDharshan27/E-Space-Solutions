# 🚀 E-Space Solutions – Mars Colonization Database Management System

With a bold vision to mark a new chapter in human history, **E-Space Solutions (Pvt.) Ltd** plans to **colonize Mars by the year 2040**. To support this monumental mission, a robust **Database Management System (DBMS)** has been initiated to ensure **accurate**, **consistent**, and **reliable data handling** throughout the entire project lifecycle.

---

## 📌 Project Overview

This system is designed to manage:

- 👨‍🚀 **Colonist Registration**  
- 👨‍👩‍👧‍👦 **Dependent Details**  
- ✈️ **E-Jet and Pilot Information**  
- 🛰️ **Trips to Mars**  
- 🏠 **Mars Colony Housing Allocation**  
- 🔧 **Job Assignments on Mars**  
- 👩‍💻 **User Roles for System Management**

The project is developed as a relational database to assist in planning, decision-making, and future system-level reporting needs.

---

## 🎯 Key Objectives

- Maintain **data integrity and consistency**.
- Register **colonists and dependents** for Mars trips.
- Manage details of **spacecrafts (E-Jets)** and **pilots**.
- Record **trip schedules and passenger manifests**.
- Allocate **Mars Colony housing** based on family composition.
- Assign **multiple jobs** to colonists in critical sectors.
- Define **user roles** for system access (Data Entry, Admins, Pilots, Superintendents).

---

## 🧾 Entities & Attributes

### 🧍 Colonist
- Mars Colonization ID (Primary Key)  
- First Name, Middle Name, Surname  
- Date of Birth, Age, Gender  
- Qualification, Civil Status  
- Earth Address, Contact No  
- Family Member Count  
- Trip ID (FK), Colony Lot Number (FK)

### 👪 Dependents (linked to Colonist)
- Full Name  
- Date of Birth, Age, Gender  
- Relationship to Colonist

### ✈️ E-Jet
- Jet Code (Primary Key)  
- Number of Seats, Engine Power, Made Year  
- Weight, Power Source  
- Jet Type:
  - 🔋 Nuclear Engine Only  
  - 🌍 Hybrid (Nuclear + Hydro Splitter)  
  - 💧 Hydro-Nuclear Engine

### 🧑‍✈️ Pilot / Astronomer
- Astronomer ID (Primary Key)  
- Full Name, Space Hours  
- Rank / Designation  
- Qualifications (can be multiple)  
- Assigned Jet (FK)

### 🌌 Trip
- Trip ID (Primary Key)  
- Jet Code (FK)  
- Launch Date  
- Return Date  
- Assigned Colonists

### 🏠 Colony House
- Colony Lot Number (Primary Key)  
- No. of Rooms  
- Square Feet  
- Occupied By Colonist(s)

### 🛠️ Job
- Job ID (Primary Key)  
- Job Title (e.g., Construction, Medical, Education)  
- Assigned to multiple colonists

### 👨‍💻 System Users
- User ID  
- Role: Data Entry Operator / System Admin / Pilot / Colony Superintendent  
- Access Scope Based on Role

---

## 🧰 Technologies Used

- 🗃️ **Database**: SQL Server  
- ⚙️ **Backend (Optional for future phases)**: ASP.NET / Node.js  
- 🖥️ **Frontend (Optional for future phases)**: React / WinForms  
- 🔐 **Security**: Role-based Access  
- 📝 **ER Diagram & Normalized Tables**: 3NF or higher  

---

## 📋 Sample Reports (Future Phase)

🔍 Reports to be generated in the next development phase:

- ✅ Jet Detail Report  
- ✈️ Trip Detail Report with Passenger List  
- 🧑‍🚀 Colonist & Dependent Details Report  
- 🏡 Colony House Assignment Report  
- 🛠️ Job Assignment Report  
- 📜 Pilot & Jet Assignment Logs

---



## 👨‍💼 Roles & Responsibilities

- 📥 Data Entry Operators: Input colonist, dependent, and trip info
- 🛠️ System Admin: Generate reports, manage jets, trips, and database
- 👨‍✈️ Pilots: View trip data & passenger manifest
- 🏠 Colony Superintendent: View housing and job allocation reports
 
---

## 🧑‍🚀 Future Enhancements

- Web-based interface for global access
- AI integration for optimal job/housing assignment
- Colonist health & psychological evaluation data
- Reporting dashboards and visual analytics

---

## 🤝 Author

👨‍💻 Mohana Dharshan
- 🐙 [GitHub](https://github.com/MDharshan27)
- 💼 [LinkedIn](https://www.linkedin.com/in/mdharshan)
- 🌐 [Portfolio Website](https://mdharshan27.github.io/Protfolio/)
