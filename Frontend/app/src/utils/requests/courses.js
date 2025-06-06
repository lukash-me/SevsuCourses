export async function createCourse(request) {
    try {
        const response = await fetch(`http://localhost:5036/Course`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(request),
        });
    const result = await response.json();

    return result;
    }
    catch (error) {
        console.error("Problem with server:", error);
    }   
}

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

export async function getAllCourses() {
    try {
        const response = await fetch('http://localhost:5036/Course');
        const result = await response.json();

        return result;
    } 
    catch (error) {
        console.error("Problem with server:", error);
    }
}

export async function getCoursesOnPage(pageNumber) {
    try {
        const response = await fetch(`http://localhost:5036/Course/page/${pageNumber}`);
        const result = await response.json();

        return result;
    } 
    catch (error) {
        console.error("Problem with server:", error);
    }
}

export async function getCourseInfo(courseId) {
    try {
        const response = await fetch(`http://localhost:5036/Course/${courseId}`);
        const result = await response.json();

        return result;
    } 
    catch (error) {
        console.error("Problem with server:", error);
    }
}

export async function updateCourseInfo(courseId, request) {
    try {
        const response = await fetch(`http://localhost:5036/Course/main-info/${courseId}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
        },
            body: JSON.stringify(request),
        });
        const result = await response.json();
        
        return result;
    }
    catch (error) {
        console.error("Problem with server:", error);
    }
}

export async function deleteCourse(courseId) {
    try {
        const response = await fetch(`http://localhost:5036/Course/${courseId}`, {
            method: "DELETE"
        });
        const result = await response.json();

        return result;
    } 
    catch (error) {
        console.error("Problem with server:", error);
    }
}