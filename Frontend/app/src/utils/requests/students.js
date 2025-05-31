export async function getStudentsInfo(request){
    try {
        const response = await fetch(
            `http://localhost:5036/Student/all-id-fio-status`,
            {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(request),
            }
        );
        const result = await response.json();

        return result;
    } 
    catch (error) {
        console.error('Problem with server:', error);
    }
}

export async function getStudentMainInfo(studentId) {
    try {
        const response = await fetch(`http://localhost:5036/Student/main-info/${studentId}`);
        const result = await response.json();

        return result;
    } 
    catch (error) {
        console.error("Problem with server:", error);
    }
}