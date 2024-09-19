export interface User {
  id: string,
  userName: string,
  fname: string,
  lname: string,
  email: string,
  phoneNumber: string,
  managerId?: string,
  managerName: string,
  roleId?: string
  roleName: string
}
