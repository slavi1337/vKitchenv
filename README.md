# vKitchenv – virtual Kitchen environment

vKitchenv is a desktop application designed for managing ingredients in your home kitchen and browsing public recipes shared by other users.
It helps users stay organized by allowing them to see which ingredients they currently have, and what meals they can prepare using those ingredients—even if they are not at home at the moment.
The app is intended for users who want to improve their meal planning, try new recipes, and share their own creations with the community.

## Database Setup

1. Download and install MySQL Server and MySQL Workbench (version 8.0.41 or newer is recommended).

2. Open MySQL Workbench, then go to File -> Open SQL Script and select 1GenerisanjeSeme.sql, then run it (lightning bolt icon).

3. Refresh the Schemas tab and make sure a new schema called mydb appears.

4. Open and run the second script: 2PoglediTrigeriProcedure.sql, then refresh again.

5. To insert test data, open and run 3TestniPodaci.sql.

6. Verify that tables are populated and no errors appear (ignore yellow or green messages; only red icons indicate errors).

7. If everything executed correctly, your database is ready for testing the application.

## Application Setup

1. Open the solution: vKitchenv -> vKitchenv.sln using Visual Studio with .NET 8.0 and Windows Forms support.

2. In App.config, configure the connection string:
   Update the password (Pwd) to match your MySQL server password.

3. Restore NuGet packages:
   Right-click the solution > Restore NuGet Packages
   Check if MySql.Data appears under Dependencies -> Packages

4. Build the project: Build -> Build Solution (or press F6)

5. Run the application.

6. Once the app starts, you can register or log in. If no errors occur, your environment is correctly configured.

### Screenshots

![home](https://github.com/user-attachments/assets/10aa79d3-7dd1-47fe-b653-a0bdfda40e0a)

![recepti](https://github.com/user-attachments/assets/bb8846f5-d95b-4f5b-a532-62aa47296f8e)

![mojprof](https://github.com/user-attachments/assets/56f8bd60-8483-43dd-afb5-87bee7f57e40)

![notif](https://github.com/user-attachments/assets/a4cc159f-d8b5-4685-9836-f38c40361e53)

![ana](https://github.com/user-attachments/assets/3f7cbc40-1698-4d99-86e6-14faa9c144cb)


