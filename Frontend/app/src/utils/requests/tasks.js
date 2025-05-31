export async function getTask(taskId) {
    try {
        const response = await fetch(`http://localhost:5036/Task/${taskId}`);
        const result = await response.json();

        return result;
    } 
    catch (error) {
        console.error("Problem with server:", error);
    }
}

export async function getTasksByTheme(themeId) {
    try {
        const response = await fetch(`http://localhost:5036/Task/theme/${themeId}`);
        const result = await response.json();

        return result;
    } 
    catch (error) {
        console.error("Problem with server:", error);
    }
}

export async function createTask(request) {
    try {
        const response = await fetch(`http://localhost:5036/Task`, {
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

export async function deleteTask(taskId) {
    try {
        const response = await fetch(`http://localhost:5036/Task/${taskId}`, {
            method: "DELETE"
        });
        const result = await response.json();

        return result;
    } 
    catch (error) {
        console.error("Problem with server:", error);
    }
}