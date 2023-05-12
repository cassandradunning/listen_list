import React, { useState } from "react";
import { Button, Form, FormGroup, Label, Input } from "reactstrap";
import { useNavigate } from "react-router-dom";
import { register } from "../modules/authManager";

export default function Register() {
  const navigate = useNavigate();

  const [name, setName] = useState();
  const [email, setEmail] = useState();
  const [username, setUsername] = useState();
  const [bio, setBio] = useState();
  const [image, setImage] = useState();
  const [password, setPassword] = useState();
  const [confirmPassword, setConfirmPassword] = useState();

  const registerClick = (e) => {
    e.preventDefault();
    if (password && password !== confirmPassword) {
      alert("Passwords don't match. Do better.");
    } else {
      const userProfile = { name, email, username, bio, image };
      register(userProfile, password).then(() => navigate("/"));
    }
  };

  return (
    <Form onSubmit={registerClick}>
      <fieldset>
        <FormGroup>
          <Label htmlFor="name">Name</Label>
          <Input
            id="name"
            type="text"
            autoFocus
            onChange={(e) => setName(e.target.value)}
          />
        </FormGroup>
        <FormGroup>
          <Label htmlFor="username">Username</Label>
          <Input
            id="username"
            type="text"
            autoFocus
            onChange={(e) => setUsername(e.target.value)}
          />
        </FormGroup>
        <FormGroup>
          <Label for="email">Email</Label>
          <Input
            id="email"
            type="text"
            onChange={(e) => setEmail(e.target.value)}
          />
        </FormGroup>
        <FormGroup>
          <Label htmlFor="bio">Bio</Label>
          <Input
            id="bio"
            type="text"
            autoFocus
            onChange={(e) => setBio(e.target.value)}
          />
        </FormGroup>
        <FormGroup>
          <Label htmlFor="imageUrl">Image URL</Label>
          <Input
            id="imageURL"
            type="text"
            autoFocus
            onChange={(e) => setImage(e.target.value)}
          />
        </FormGroup>
        <FormGroup>
          <Label for="password">Password</Label>
          <Input
            id="password"
            type="password"
            onChange={(e) => setPassword(e.target.value)}
          />
        </FormGroup>
        <FormGroup>
          <Label for="confirmPassword">Confirm Password</Label>
          <Input
            id="confirmPassword"
            type="password"
            onChange={(e) => setConfirmPassword(e.target.value)}
          />
        </FormGroup>
        <FormGroup>
          <Button>Register</Button>
        </FormGroup>
      </fieldset>
    </Form>
  );
}
