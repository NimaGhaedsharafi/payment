<?php

Route::post('payment', function(Illuminate\Http\Request $req){
	
	$client = new \SoapClient("http://sandbox.parspal.com/WebService.asmx?wsdl");

	$MerchantID = '100001';
	$password = 'abcdeFGHI';

	$result = $client->RequestPayment([
		'MerchantID' => $MerchantID,
		'Password' => $password,
		'Price' => 10000,
		'Description' => '',
		'Paymenter' => $req->get('name'),
		'Email' => $req->get('email'),
		'Mobile' => '',
		'ResNumber' => '1234',
		'ReturnPath' => \URL::to('verfiy')
	]);

	$payPath = $result->RequestPaymentResult->PaymentPath;
	$status = $result->RequestPaymentResult->ResultStatus;

	if($status == 'Succeed') {
		return "<!DOCTYPE html><html><body onload=\"javascript:document.location='$payPath'\">Connecting...</body></html>";
	} else {
		return 'failed';
	}
});

Route::post('verfiy', function(Illuminate\Http\Request $req){
	
	if($req->get('status') == 100) {

		$client = new \SoapClient("http://sandbox.parspal.com/WebService.asmx?wsdl");

		$MerchantID = '100001';
		$password = 'abcdeFGHI';

		$result = $client->verifyPayment([
			'MerchantID' => $MerchantID,
			'Password' => $password,
			'Price' => 10000,
			'RefNum' => $req->get('refnumber')
		]);

		$status = $result->verifyPaymentResult->ResultStatus;

		if($status == 'success') {
			return "<button onclick=\"CSharp.showMeSmth()\" >Click Here to show you something, cool?</button>";
		} else {
			return 'unsuccess';
		}
	} else {
		return 'failed';
	}
});