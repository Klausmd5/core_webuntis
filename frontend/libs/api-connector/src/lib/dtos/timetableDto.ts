export class TimetableDto {
  start: string;
  end: string;
  id: number;
  classIds?: number[];
  teacherIds?: number[];
  subjectIds?: number[];
  roomIds?: number[];
  lessonType?: string;
  code?: string;
  substitutionText?: string;


  constructor(start: string, end: string, id: number, classIds: number[], teacherIds: number[], subjectIds: number[], roomIds: number[], lessonType: string, code: string, substitutionText: string) {
    this.start = start;
    this.end = end;
    this.id = id;
    this.classIds = classIds;
    this.teacherIds = teacherIds;
    this.subjectIds = subjectIds;
    this.roomIds = roomIds;
    this.lessonType = lessonType;
    this.code = code;
    this.substitutionText = substitutionText;
  }
}
