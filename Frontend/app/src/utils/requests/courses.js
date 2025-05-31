export async function getCourseTitleAndTeacher(courseId) {
    try {
        const response = await fetch(`http://localhost:5036/Course/title-teacher/${courseId}`);
        const result = await response.json();

        return result;
    } 
    catch (error) {
        console.error("Problem with server:", error);
    }
}