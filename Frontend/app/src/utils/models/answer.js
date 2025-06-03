import { ref } from 'vue';

export class Answer {

    id = ref();
    taskId = ref();
    studentId = ref();
    mark = ref();
    replyText = ref();
    answerText = ref();
    dateSent = ref();
    dateReplied = ref();

    constructor(id=null, taskId=null, studentId=null, mark=null, replyText=null, answerText=null, dateSent=null, dateReplied=null) {
        this.id = id;
        this.taskId = taskId;
        this.studentId = studentId;
        this.mark = mark;
        this.replyText = replyText;
        this.answerText = answerText;
        this.dateSent = dateSent;
        this.dateReplied = dateReplied;
    }
}

