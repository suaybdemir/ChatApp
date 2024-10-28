import React from 'react'
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import Divider from '@mui/material/Divider';
import ListItemText from '@mui/material/ListItemText';
import ListItemAvatar from '@mui/material/ListItemAvatar';
import Avatar from '@mui/material/Avatar';
import Typography from '@mui/material/Typography';

function UserMessage({ message }) {
    return (
        <List sx={{ width: '100%', maxWidth: 360, backgroundColor: "#DCCCFF" }}>
            <ListItem alignItems="flex-start">

                <ListItemText

                    secondary={
                        <React.Fragment>
                            <Typography
                                component="span"
                                variant="body2"
                                sx={{ color: 'text.primary', display: 'inline' }}
                            >
                                {message}
                            </Typography>
                        </React.Fragment>
                    }
                />
            </ListItem>
        </List>
    )
}

export default UserMessage