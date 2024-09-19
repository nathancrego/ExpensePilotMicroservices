export interface EditUser {
  userName: string,
  fname: string,
  lname: string,
  email: string,
  phoneNumber: string,
  managerId: string,
  managerName: string,
  departmentId?: number | null;
  departmentName: string;
  roleName: string
}
