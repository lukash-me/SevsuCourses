export async function getAnswer(taskId, studentId) {
    try {
        const response = await fetch(
            `http://localhost:5036/Answer/last/taskId=${taskId}&studentId=${studentId}`
        );
        const result = await response.json();

        return result;
    } 
    catch (error) {
        console.error("Problem with server:", error);
    }
}

 export async function createAnswer(request) {
    try {
        const response = await fetch(
            `http://localhost:5036/Answer`,
            {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(request),
            }
        ); 
        const result = response.json();

        return result;
    } 
    catch (error) {
        console.error("Problem with server:", error);
    }
}

export async function createReply(answerId, request) {
    try {
        const response = await fetch(
            `http://localhost:5036/Answer/${answerId}/reply-mark`,
            {
                method: "PUT",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(request),
            }
        );
        const result = await response.json();

        return result;
    } 
    catch (error) {
        console.error("Problem with server:", error);
    }
}