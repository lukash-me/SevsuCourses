export async function getSolution(taskId) {
    try {
        const response = await fetch(`http://localhost:5036/Solution/answer/${taskId}`);
        const result = await response.json();

        return result;
    } 
    catch (error) {
        console.error("Problem with server:", error);
    }
}