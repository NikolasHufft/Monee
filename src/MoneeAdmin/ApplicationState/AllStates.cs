namespace MoneeAdmin.ApplicationState
{
    public class AllStates
    {
        // Scope action
        public Action? Action { get; set; }

        // General Department
        public bool ShowGeneralDepartment { get; set; }
        public void GeneralDepartmentClicked()
        {
            ResetAll();
            ShowGeneralDepartment = true;
            Action?.Invoke();
        }

        // Department
        public bool ShowDepartment { get; set; }
        public void DepartmentClicked()
        {
            ResetAll();
            ShowDepartment = true;
            Action?.Invoke();
        }

        // Branch
        public bool ShowBranch { get; set; }
        public void BranchClicked()
        {
            ResetAll();
            ShowBranch = true;
            Action?.Invoke();
        }

        // Country
        public bool ShowCountry { get; set; }
        public void CountryClicked()
        {
            ResetAll();
            ShowCountry = true;
            Action?.Invoke();
        }

        // City
        public bool ShowCity { get; set; }
        public void CityClicked()
        {
            ResetAll();
            ShowCity = true;
            Action?.Invoke();
        }

        // Town
        public bool ShowTown { get; set; }
        public void TownClicked()
        {
            ResetAll();
            ShowTown = true;
            Action?.Invoke();
        }

        // User
        public bool ShowUser { get; set; }
        public void UserClicked()
        {
            ResetAll();
            ShowUser = true;
            Action?.Invoke();
        }

        // Employee
        public bool ShowEmployee { get; set; } = true;
        public void EmployeeClicked()
        {
            ResetAll();
            ShowEmployee = true;
            Action?.Invoke();
        }

        public bool ShowHealth { get; set; } = true;
        public void HealthClicked()
        {
            ResetAll();
            ShowHealth = true;
            Action?.Invoke();
        }

        private void ResetAll()
        {
            ShowGeneralDepartment = false;
            ShowDepartment = false;
            ShowBranch = false;
            ShowCountry = false;
            ShowCity = false;
            ShowTown = false;
            ShowUser = false;
            ShowEmployee = false;            
        }
    }
}
