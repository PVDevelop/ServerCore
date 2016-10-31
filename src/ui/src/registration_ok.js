import React from "react";
import { browserHistory } from "react-router";
import Form from 'react-bootstrap/lib/Form';
import Button from 'react-bootstrap/lib/Button';
import Alert from 'react-bootstrap/lib/Alert';

export default class RegistrationOk extends React.Component{
	constructor(props)
	{
		super(props);
		this.onOkButtonClicked = this.onOkButtonClicked.bind(this);
	}

	onOkButtonClicked(e)
	{
		browserHistory.push('/');
	}

	render() {
		return(
			<Alert bsStyle="success">
				<h4>Пользователь успешно создан.</h4>
				<p>Подтверждение о регистрации отправлено на указанный почтовый адрес.</p>
				<Button 
					bsStyle="primary"
					onClick = {this.onOkButtonClicked}>
					OK
				</Button>
			</Alert>
		);
	}
}