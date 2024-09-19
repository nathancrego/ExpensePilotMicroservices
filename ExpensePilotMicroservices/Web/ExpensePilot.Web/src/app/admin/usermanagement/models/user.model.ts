export interface User {
  id: string,
  userName: string,
  fname: string,
  lname: string,
  email: string,
  phoneNumber: string,
  managerId?: string,
  managerName: string,
  departmentId?: number,
  departmentName: string,
  roleId?: string
  roleName: string
}
