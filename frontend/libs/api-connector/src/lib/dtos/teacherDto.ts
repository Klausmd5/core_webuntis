export class TeacherDto {
  id: number;
  name?: string;
  firstName?: string;
  lastName?: string;

  constructor(id: number, name?: string, firstName?: string, lastName?: string) {
    this.id = id;
    this.name = name;
    this.firstName = firstName;
    this.lastName = lastName;
  }
}
