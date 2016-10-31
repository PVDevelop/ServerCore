import React from "react";
import { browserHistory } from "react-router";

import Form from 'react-bootstrap/lib/Form';
import FormControl from 'react-bootstrap/lib/FormControl';
import FormGroup from 'react-bootstrap/lib/FormGroup';
import ControlLabel from 'react-bootstrap/lib/ControlLabel';
import Button from 'react-bootstrap/lib/Button';
import Panel from 'react-bootstrap/lib/Panel';

import RegistrationOk from "./registration_ok";

export default class RegisterForm extends React.Component {
	constructor(props) {
		super(props);
		this.state =
			{
				email: '',
				password: '',
				showOk: false
			};

		this.onEmailChange = this.onEmailChange.bind(this);
		this.onPasswordChange = this.onPasswordChange.bind(this);
		this.onRegisterButtonClicked = this.onRegisterButtonClicked.bind(this);
		this.onRegistrationOk = this.onRegistrationOk.bind(this);
	}

	onEmailChange(event) {
		this.setState({ email: event.target.value });
	}

	onPasswordChange(event) {
		this.setState({ password: event.target.value });
	}

	onRegistrationOk() {
		this.setState({ showOk: true });
	}

	onRegisterButtonClicked(e) {
		e.preventDefault();

		var data = {
			email: this.state.email,
			password: this.state.password
		};

		var json = JSON.stringify(data);

		var options = {
			method: "post",
			headers: {
				"Accept": "application/json",
				"Content-Type": "application/json"
			},
			body: json
		};

		var url = "http://localhost:8000/api/users";

		fetch(url, options)
			.then(response => {
				if (response.status == 200) {
					this.onRegistrationOk();
				}
				else {
					alert(response.status);
				}
			})
			.catch(err => alert(err));
	}

	render() {
		return (
			<Panel header="Создание нового пользователя">
				<Form>
					<FormGroup>
						<ControlLabel>E-mail</ControlLabel>
						<FormControl
							type="email"
							placeholder="Введите почтовый адрес"
							value={this.state.email}
							onChange={this.onEmailChange} />

						<ControlLabel>Password</ControlLabel>
						<FormControl
							type="password"
							placeholder="Введите пароль"
							value={this.state.password}
							onChange={this.onPasswordChange} />

						<Button
							type="submit"
							onClick={this.onRegisterButtonClicked}>Создать</Button>
					</FormGroup>
					{this.state.showOk ? <RegistrationOk /> : null}
				</Form>
			</Panel>
		);
	}
}