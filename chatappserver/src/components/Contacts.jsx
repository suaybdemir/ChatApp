import { Box, List, ListItem, ListItemButton, ListItemIcon, ListItemText } from '@mui/material'
import InboxIcon from '@mui/icons-material/Inbox';
import React, { useEffect, useState } from 'react'
import { useDispatch, useSelector } from "react-redux"
import { getContacts } from '../redux/slices/Contacts';
import { HubConnectionBuilder } from '@microsoft/signalr';

function Contacts() {


    const contacts = useSelector((state) => state.contact.contacts)



    const dispatch = useDispatch()

    useEffect(() => {
        dispatch(getContacts())
    }, [])





    return (
        <Box sx={{ backgroundColor: "#8FBDFF" }}>
            <List>
                {
                    contacts && contacts.map((contact) => (
                        <ListItem key={contact.id} disablePadding>
                            <ListItemButton>
                                <ListItemIcon>
                                    <InboxIcon />
                                </ListItemIcon>
                                <ListItemText primary={`${contact.name}`} />
                            </ListItemButton>
                        </ListItem>
                    ))

                }



            </List>
        </Box>
    )
}

export default Contacts