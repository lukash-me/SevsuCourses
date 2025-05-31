export async function getAllGroupsOnCourse(courseId) {
    try {
        const response = await fetch(`http://localhost:5036/Group/course/${courseId}`);
        const result = await response.json();

        return result;
    }
    catch (error) {
        console.error("Problem with server:", error);
    }
}

export async function getMentorGroupsOnCourse(mentorId, courseId) {
    try {
        const response = await fetch(`http://localhost:5036/Group/${mentorId}&${courseId}`);
        const result = await response.json();

        return result;
    }
    catch (error) {
        console.error("Problem with server:", error);
    }
}