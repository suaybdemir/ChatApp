
import React, { useState } from 'react'
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';
import IconButton from '@mui/material/IconButton';
import MenuIcon from '@mui/icons-material/Menu';
import ConnectWithoutContactIcon from '@mui/icons-material/ConnectWithoutContact';
import ChatHeader from './ChatHeader';
import ChatBody from './ChatBody';
import ChatInput from './ChatInput';
import "../style/chat.css"

function Chat() {



    return (
        <Box className="Box">

            <Box className="ChatBody">
                <ChatHeader />
            </Box>

            <Box className="ChatBody">
                <ChatBody />
            </Box>



        </Box>

    )
}

export default Chat