import React, { useState, useEffect, createContext } from 'react';
import { Navigate } from 'react-router-dom';


const UserContext = createContext({});

interface User {
    email: string;
}

function AuthorizeView(props: { children: React.ReactNode }) {

    const [authorized, setAuthorized] = useState<boolean>(false);
    const [loading, setLoading] = useState<boolean>(true); // Add a loading state
    let emptyuser: User = { email: "" };

    const [user, setUser] = useState(emptyuser);


    useEffect(() => {
        // Get the cookie value
        let retryCount = 0; // Initialize the retry count
        let maxRetries = 10; // Set the maximum number of retries
        let delay: number = 1000; // Set the delay in milliseconds

        // Define a delay function that returns a promise
        function wait(delay: number) {
            return new Promise((resolve) => setTimeout(resolve, delay));
        }

        // Define a fetch function that retries until status 200 or 401
        async function fetchWithRetry(url: string, options: any) {
            try {
                // Make the fetch request
                let response = await fetch(url, options);

                // Check the status code
                if (response.status == 200) {
                    console.log("Authorized");
                    let j: any = await response.json();
                    setUser({ email: j.email });
                    setAuthorized(true);
                    return response; // Return the response
                } else if (response.status == 401) {
                    console.log("Unauthorized");
                    return response; // Return the response
                } else {
                    // Throw an error to trigger the catch block
                    throw new Error("" + response.status);
                }
            } catch (error) {
                // Increment the retry count
                retryCount++;
                // Check if the retry limit is reached
                if (retryCount > maxRetries) {
                    // Stop retrying and rethrow the error
                    throw error;
                } else {
                    // Wait for some time and retry
                    await wait(delay);
                    return fetchWithRetry(url, options);
                }
            }
        }

        // Call the fetch function with retry logic
        fetchWithRetry("/pingauth", {
            method: "GET",
        })
            .catch((error) => {
                // Handle the final error
                console.log(error.message);
            })
            .finally(() => {
                setLoading(false);  // Set loading to false when the fetch is done
            });
    }, []);


    if (loading) {
        return (
            <>
                <p>Loading...</p>
            </>
        );
    }
    else {
        if (authorized && !loading) {
            return (
                <>
                    <UserContext.Provider value={user}>{props.children}</UserContext.Provider>
                </>
            );
        } else {
            return (
                <>
                    <Navigate to="/login" />
                </>
            )
        }
    }

}

export function AuthorizedUser(props: { value: string }) {
    // Consume the username from the UserContext
    const user: any = React.useContext(UserContext);

    // Display the username in a h1 tag
    if (props.value == "email")
        return <>{user.email}</>;
    else
        return <></>
}

export default AuthorizeView;