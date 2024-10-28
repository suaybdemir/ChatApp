import React, { useEffect, useRef, useState } from 'react';
import Box from '@mui/material/Box';
import ContactMessage from './ContactMessage';
import UserMessage from './UserMessage';
import '../style/chatBody.css';
import { Button, TextField } from '@mui/material';
import { useDispatch, useSelector } from 'react-redux';
import SendIcon from '@mui/icons-material/Send';
import { useNavigate } from 'react-router-dom';
import { getMessages, addMessage } from '../redux/slices/messageSlice';
import { HubConnectionBuilder } from '@microsoft/signalr';

function ChatBody() {
    const navigate = useNavigate();
    const dispatch = useDispatch();
    const user = useSelector((state) => state.user.user); // Adjust as necessary
    const messages = useSelector((state) => state.message.messages);

    const [connection, setConnection] = useState(null);
    const [textInput, setTextInput] = useState('');

    // Access user_id based on actual structure
    const user_id = user?.user?.id; // Use the id from the user object

    // Establish SignalR connection when user is available
    useEffect(() => {
        if (user_id) { // Ensure user_id is valid
            const newConnection = new HubConnectionBuilder()
                .withUrl('https://localhost:44335/chat-hub') // Backend SignalR hub URL
                .build();

            newConnection.start()
                .then(() => {
                    console.log('Connected to SignalR hub');
                    setConnection(newConnection);
                })
                .catch(err => console.error('Error connecting to hub:', err));
        }
    }, [user_id]); // Depend on user_id

    // Handle incoming messages via SignalR
    useEffect(() => {
        if (connection) {
            connection.on('ReceiveMessage', (user_id, message) => {
                console.log('Received message:', message);
                // Dispatch the received message to Redux store
                dispatch(addMessage({ userId: user_id, message }));
            });
        }
    }, [connection, dispatch]);

    // Fetch initial messages from the server
    useEffect(() => {
        dispatch(getMessages());
    }, [dispatch]);

    // Handle sending messages
    const sendMessage = async () => {
        if (!user_id || textInput.trim() === '') return; // Ensure user_id is valid

        if (connection) {
            try {
                await connection.invoke('SendMessage', user_id, textInput); // Call your SendMessage method on the server
                setTextInput(''); // Clear input after sending
            } catch (error) {
                console.error('Error sending message:', error);
            }
        }
    };

    // Redirect to login if user is not logged in
    useEffect(() => {
        if (!user) {
            navigate('/login');
        }
    }, [user, navigate]);

    // Render loading state if user is not available
    if (!user) {
        return <div>Loading...</div>; // Adjust as needed
    }

    const chatEndRef = useRef(null);

    useEffect(() => {
        if (chatEndRef.current) {
            chatEndRef.current.scrollIntoView({ behavior: 'smooth' });
        }
    }, [messages]); // Scroll to bottom whenever messages change

    return (
        <Box className="chat-body">
            <Box sx={{ paddingTop: '20px', height: '500px', overflowY: 'auto' }}>
                {messages && messages.map((message, index) => (
                    message.userId === user_id ? (
                        <Box
                            key={index}
                            className="user-message"
                            sx={{
                                display: 'flex',
                                justifyContent: 'flex-end',
                                marginTop: '10px',
                                marginRight: '20px',
                            }}
                        >
                            <UserMessage message={message.message} />
                        </Box>
                    ) : (
                        <Box key={index} sx={{ fontSize: '20px', marginLeft: '20px' }}>
                            <ContactMessage message={message.message} />
                        </Box>
                    )
                ))}
                <div ref={chatEndRef} /> {/* This empty div helps with scrolling */}
            </Box>

            <Box sx={{ marginTop: '10px' }} className="ChatInput">
                <Box
                    className="chat-input"
                    sx={{ display: 'flex', flexDirection: 'row', backgroundColor: 'white' }}
                >
                    <TextField
                        value={textInput}
                        onChange={(e) => setTextInput(e.target.value)}
                        fullWidth
                        id="filled-basic"
                        label="Enter your text here.."
                        variant="filled"
                    />
                    <Button
                        onClick={sendMessage}
                        sx={{ backgroundColor: 'grey' }}
                        variant="contained"
                    >
                        SEND <SendIcon />
                    </Button>
                </Box>
            </Box>
        </Box>
    );
};

export default ChatBody;
