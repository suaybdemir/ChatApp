import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import axios from "axios";

const initialState = {
    messages: [],  // Should be an array to handle multiple messages
}

// Async action to fetch initial chat messages
export const getMessages = createAsyncThunk(
    'messages/getMessages',
    async () => {
        const response = await axios.get("https://localhost:44335/api/Chats/GetChats?userId=ed9faddd-0c89-4bb4-9e58-da6fbc6c7830&toUserId=3d57495a-e4ac-4e6b-a946-bc475327d7aa");
        return response.data;
    },
)

// Create the message slice
export const messageSlice = createSlice({
    name: 'message',
    initialState,
    reducers: {
        // Action to handle adding a new message
        addMessage: (state, action) => {
            state.messages.push(action.payload); // Add the new message to the messages array
        },
    },
    extraReducers: (builder) => {
        // Handle the case when messages are successfully fetched
        builder.addCase(getMessages.fulfilled, (state, action) => {
            state.messages = action.payload;
        });
    },
})

// Export the addMessage action creator
export const { addMessage } = messageSlice.actions;

export default messageSlice.reducer;
