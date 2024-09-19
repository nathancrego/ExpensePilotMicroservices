export interface AddUser {
  fname: string,
  lname: string,
  email: string,
  phoneNumber: string,
  managerId?: string|null,
  role: {
    id: string,
    roleName: string
  }
}
