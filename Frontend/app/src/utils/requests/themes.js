export async function getTheme(themeId) {
    try {
        const response = await fetch(`http://localhost:5036/Theme/${themeId}`);
        const result = await response.json();

        return result
    
    } catch (error) {
        console.error("Problem with server:", error);
    }
}

export async function getAllThemes(courseId) {
    try {
        const response = await fetch(`http://localhost:5036/Theme/all/${courseId}`);
        const result = await response.json();

        return result;
    }
    catch (error) {
        console.error("Problem with server:", error);
    }   
}

export async function createTheme(courseId, request) {
    try {
        const response = await fetch(`http://localhost:5036/Theme/${courseId}`, {
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

export async function updateTheme(themeId, request) {
    try {
        const response = await fetch(`http://localhost:5036/Theme/main-info/${themeId}`, {
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

export async function deleteTheme(themeId) {
    try {
        const response = await fetch(`http://localhost:5036/Theme/${themeId}`, {
            method: "DELETE"
        });
        const result = await response.json();
        
        return result;
    } 
    catch (error) {
        console.error("Problem with server:", error);
    }
}