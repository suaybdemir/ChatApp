import React, { useEffect, useState } from 'react'
import Box from '@mui/material/Box';
import ContactMessage from './ContactMessage';
import UserMessage from './UserMessage';
import "../style/chatBody.css"
import { Button, Stack, TextField } from '@mui/material';
import { useDispatch, useSelector } from 'react-redux';
import SendIcon from '@mui/icons-material/Send';
import { useNavigate } from 'react-router-dom';
import { getMessages } from '../redux/slices/messageSlice';

import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";



function ChatBody() {



    const navigate = useNavigate()

    const dispatch = useDispatch()

    const user = useSelector((state) => state.user.user)

    const messages = useSelector((state) => state.message.messages)


    useEffect(() => {
        dispatch(getMessages())
    }, [dispatch])




    const [textInput, setTextInput] = useState()

    const sendMessage = () => {
        setTextInput("")
    }

    useEffect(() => {
        if (user == null) {
            navigate("/login")
        }
    })






    return (
        <Box className="chat-body" >
            <Box sx={{ paddingTop: "20px", height: "500px", }}>

                {
                    // Kullanıcı varsa ve mesajlar varsa, her mesajı kontrol et
                    messages && user && messages.map((message) => (
                        message.userId === user.user.id ? (

                            <Box className="user-message" sx={{
                                display: 'flex',
                                justifyContent: 'flex-end',  // Sağ tarafa hizalamak
                                marginTop: '10px',  // Gerekirse arayı açabilirsiniz,
                                marginRight: "20px"
                            }}>
                                <UserMessage message={message.message} sx={{ display: "block" }} />
                            </Box>

                        ) : (
                            <Box sx={{ fontSize: "20px", marginLeft: "20px" }}>
                                <ContactMessage message={message.message} />
                            </Box>
                        )
                    ))
                }
                <Stack>





                </Stack>

            </Box>

            <Box sx={{ marginTop: "10px" }} className="ChatInput">
                <Box className="chat-input" sx={{ display: "flex", flexDirection: "row", backgroundColor: "white", }}>
                    <TextField value={textInput} onChange={(text) => setTextInput(text.target.value)} fullWidth id="filled-basic" label="Enter your text here.." variant="filled" />
                    <Button onClick={() => sendMessage()} sx={{ backgroundColor: "grey", }} variant="contained">SEND
                        <SendIcon />

                    </Button>

                </Box>

            </Box>

        </Box>

    )
}

export default ChatBody