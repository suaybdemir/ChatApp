import React from 'react'
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';
import IconButton from '@mui/material/IconButton';
import MenuIcon from '@mui/icons-material/Menu';
import ConnectWithoutContactIcon from '@mui/icons-material/ConnectWithoutContact';

function ChatHeader() {
    return (
        <AppBar sx={{ backgroundColor: "#00A2E8" }} position="static">
            <Toolbar>
                <IconButton
                    size="large"
                    edge="start"
                    color="inherit"
                    aria-label="menu"
                    sx={{ mr: 2 }}
                >
                    <ConnectWithoutContactIcon />
                </IconButton>
                <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                    Contact Name
                </Typography>
                <Button color="inherit">Settings</Button>
            </Toolbar>
        </AppBar>
    )
}

export default ChatHeader