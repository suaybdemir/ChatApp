import { createSlice, createAsyncThunk } from '@reduxjs/toolkit'
import axios from "axios"


const initialState = {
    messages: null,
}

export const getMessages = createAsyncThunk(
    'messages/getMessages',
    async () => {

        const response = await axios.get("https://localhost:44335/api/Chats/GetChats?userId=ed9faddd-0c89-4bb4-9e58-da6fbc6c7830&toUserId=3d57495a-e4ac-4e6b-a946-bc475327d7aa")

        return response.data

    },
)



export const messageSlice = createSlice({
    name: 'message',
    initialState,
    reducers: {

    },
    extraReducers: (builder) => {
        // Add reducers for additional action types here, and handle loading state as needed
        builder.addCase(getMessages.fulfilled, (state, action) => {
            state.messages = action.payload
        })
    },
})

// Action creators are generated for each case reducer function
export const { } = messageSlice.actions

export default messageSlice.reducer