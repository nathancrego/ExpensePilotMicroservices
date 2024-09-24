export interface AddUser {
  fname: string,
  lname: string,
  email: string,
  phoneNumber: string,
  managerId?: string,
  departmentId?:number|null,
  role: {
    id: string,
    roleName: string
  }
}
