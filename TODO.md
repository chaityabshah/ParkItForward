
























"X" button to remove class section tr
Update event>url for classes to go to right location (Class details page)
Add [Authorize] attribute to functions
Add admin console
User roles:
- admin
- user
- public tour
Create homepage based on partial views of tasks overview and calendar
Add subject and task_type support to tasks
Settings page where user can change any of the above elements
Google Recaptcha integration
Error messages (http codes)
Prompt user when session is null
Login popup
Content overlay when user has JS disabled
UsersController
  Create
  Read
  Update
  Delete
//"PHP include"-style includes for common date-time pickers
?Completion button?

--- DONE ---
Create a new, independent registration page

TIMEZONES: (45 mins)
- Add property to user: (30 mins total)
  - Store UTC offset in new Users table column: (5 mins)
  - New: [Dropdown | "PST" -- "-0700"] (10 mins)
    - get list, display in select-dropdown menu (learn select dropdown menus)
    In: (add property to Users DTO) (15 minutes)
    - registration
    - settings page
-  front<->middle (15 minutes total)
  - Date inputs -> UTC . conversion in controller (5 mins)
  - Displaying dates -> get user timezone, use time utilities to convert from UTC to user Timezone (10 mins)
Timezone support
New register page (same list in settings page):
  - email
  - password
  - Subjects (textarea -> check for redundancies -> store in db)
  - Task types (same)
  - what timezone?
Note: New password for pramod@example.com is 'test'
casing problems for html forms
consistent forms
Calendars:
- classes:
  -
- events
