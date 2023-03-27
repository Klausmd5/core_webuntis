export class StudentDto{
  classId?: number;
  classTeacherId?: number;
  firstName: string;
  lastName: string;
  email?: string;


  constructor(classId: number, classTeacherId: number, firstName: string, lastName: string, email: string) {
    this.classId = classId;
    this.classTeacherId = classTeacherId;
    this.firstName = firstName;
    this.lastName = lastName;
    this.email = email;
  }
}
