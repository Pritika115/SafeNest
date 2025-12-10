# SafeNest – Personal Safety & Location Tracking Web App

SafeNest is a Razor Pages Web Application designed to help users stay safe by providing real-time location tracking, weather conditions, emergency contacts, and a clean UI built for simplicity and reliability.

> **Note:**  
> Our team initially tried building this project in **.NET MAUI**, but the emulator kept crashing and prevented us from running the app.  
> Because of this, we decided to move the entire project to a **Razor Pages Web Application**, which works smoothly and supports all required features.

---

## 🚀 Features

### ✔️ Real-Time Location Tracking  
- Fetches user **latitude + longitude** using browser geolocation.  
- Fetches **weather + temperature** automatically for the same coordinates.  
- Displays results instantly on the page.

### ✔️ Persistent Sensor Data  
- Sensor/location values are saved to the database and **do not disappear even after restarting** the application.

### ✔️ Weather Integration  
- Uses the OpenWeather API to retrieve:
  - Temperature  
  - Weather condition  
  - Feels-like temperature  
  - Location name  

### ✔️ Clean UI With Sidebar  
- Dark animated background  
- Sidebar navigation  
- Responsive layout for all pages  

### ✔️ Secure PIN Login  
- Users log in with a secure PIN.  
- Prevents unauthorized access.

---

## 🛠️ Tech Stack

- **C#**
- **ASP.NET Razor Pages**
- **MySQL Database**
- **OpenWeather API**
- **HTML / CSS / JavaScript**
- **Visual Studio 2022**

---

## 📡 How Location + Weather Works

1. User clicks **Get Current Location**.  
2. Browser returns latitude & longitude.  
3. The app automatically sends those coordinates to the **Weather API**.  
4. Weather + temperature + location name are displayed **together**.  
5. Data (lat, long, weather, temperature) is stored permanently in the MySQL **Sensor** table.

---

## 🗄️ Database Persistence

We created a table called: Sensor

Whenever the user fetches location + weather:

- Latitude  
- Longitude  
- Temperature  
- Weather status  
- Timestamp  

are all saved permanently.

These records **do NOT disappear** when restarting the project.

---

## 📁 Project Structure

```
SafeNest/
│── Controllers/
│   ├── HomeController.cs
│   ├── SensorController.cs
│
│── Data/
│   ├── SafeNestDbContext.cs
│   ├── safenest.db
│
│── Models/
│   ├── SensorData.cs
│
│── Pages/
│   ├── Index.cshtml
│   ├── Layout.cshtml
│
│── Services/
│   ├── LocationService.cs
│   ├── WeatherService.cs
│
│── wwwroot/
│   ├── css/
│   ├── js/
│
│── appsettings.json
│── Program.cs

```
---

## 👥 Team Members

- **Pritika**  
- **Jaskaran**  
- **Jeremiah**

---

## 📸 Screenshots
<img width="1366" height="720" alt="image" src="https://github.com/user-attachments/assets/04ecea37-32ed-4458-a411-4dc5e168769b" />
<img width="815" height="479" alt="image" src="https://github.com/user-attachments/assets/9b22dab6-9ee1-435c-bd9d-31da4a46d0c9" />
<img width="572" height="192" alt="image" src="https://github.com/user-attachments/assets/e6f87a4c-94bd-453b-a4a7-104712fb986e" />
<img width="1206" height="370" alt="image" src="https://github.com/user-attachments/assets/191be3b3-10fd-4d12-94e4-c81c8d513e7c" />


---

## 📬 How to Run

1. Clone the repository.  
2. Update MySQL connection string in `appsettings.json`.  
3. Run the application in Visual Studio.  
4. Create tables using provided SQL scripts.  
5. Launch the site → Login → Access features.

---

## 📜 License

This project is for academic use only.

---

## 🙌 Acknowledgements

Thanks to our professor and peers for guidance and support.





