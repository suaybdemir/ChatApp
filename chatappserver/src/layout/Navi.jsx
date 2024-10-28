import React, { useEffect, useState } from 'react';
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';
import { useNavigate } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { HubConnectionBuilder } from '@microsoft/signalr';
import { logOut } from "../redux/slices/userSlice"

function Navi() {
    const user = useSelector((state) => state.user.user);
    const navigate = useNavigate();
    const dispatch = useDispatch();
    const [connection, setConnection] = useState(null);

    const user_id = user?.user?.id; // Use the id from the user object


    const navigateToLogin = () => {
        navigate("/login");
    };

    // Establish SignalR connection when user is available
    useEffect(() => {
        if (user) {
            if (user.user.id) { // Ensure user_id is valid
                const newConnection = new HubConnectionBuilder()
                    .withUrl('https://localhost:44335/chat-hub') // Backend SignalR hub URL
                    .build();

                newConnection.start()
                    .then(() => {
                        console.log('Connected to SignalR hub');
                        setConnection(newConnection);
                        newConnection.invoke("Connect", user_id); // Notify server of connection
                        console.log("bağlandı")
                    })
                    .catch(err => console.error('Error connecting to hub:', err));
            } else {
                console.log("bağlanmadı")
            }
        }


        return () => {
            if (connection) {
                connection.stop();
            }
        };
    }, [user]); // Depend on user object

    const handleLogOut = async () => {

        if (connection) {
            console.log("111")
            // await connection.invoke('OnDisconnectedAsync', user_id);
            await connection.stop(); // Stop the SignalR connection
        }

        console.log("2222")
        dispatch(logOut())
        navigate("/login"); // Redirect to login
    };

    return (
        <Box sx={{ flexGrow: 1 }}>
            <AppBar className='appbar' position="static" sx={{ backgroundColor: "#f7f7f7" }}>
                <Toolbar>
                    <Typography variant="h6" component="div" sx={{ flexGrow: 1, color: "black" }}>
                        <span onClick={() => navigate("/")} className='header'>ChatAppServer</span>
                    </Typography>
                    <div style={{ display: "flex" }}>
                        {user ? (
                            <Button sx={{ color: "black" }} onClick={handleLogOut} color="inherit">Logout</Button>
                        ) : (
                            <Button sx={{ color: "black" }} onClick={navigateToLogin} color="inherit">Login</Button>
                        )}
                    </div>
                </Toolbar>
            </AppBar>
        </Box>
    );
}

export default Navi;
