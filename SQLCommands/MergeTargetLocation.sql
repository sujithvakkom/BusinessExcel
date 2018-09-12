MERGE sc_salesmanage_merchant.merchant_daily_update AS target
USING ( SELECT t.roster_id ,
               r.name ,
			   r.location_id,
               t.target_id ,
               r.start_date ,
               r.end_date ,
               t.user_id
        FROM target_m AS t INNER JOIN roster AS r ON t.roster_id = r.roster_id
        WHERE user_id IS NOT NULL
      ) AS source(roster_id ,name,location_id, target_id , start_date , end_date , user_id)
ON target.user_id = source.user_id
   AND
   target.create_time BETWEEN source.start_date AND source.end_date
    WHEN MATCHED
    THEN UPDATE SET
                    target.target_id = source.target_id,
					target.location_id = source.location_id;